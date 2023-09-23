// <copyright file="Program.cs" company="SalviePl">
// MIT License
//
// Copyright (c) 2023 Salvie Pluviem
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// </copyright>

using System.Text;
using InactivityBot.Dto;

namespace InactivityBot;

using System.Reflection;
using System.Text.Json;
using Discord;
using Discord.WebSocket;
using InactivityBot.Dto.Auth;
using InactivityBot.Mongo;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

/// <summary>
/// The main program class.
/// </summary>
public class Program
{
    private readonly Dictionary<LogSeverity, LogLevel> logLevels = new()
    {
        [LogSeverity.Critical] = LogLevel.Critical,
        [LogSeverity.Debug] = LogLevel.Debug,
        [LogSeverity.Error] = LogLevel.Error,
        [LogSeverity.Info] = LogLevel.Information,
        [LogSeverity.Verbose] = LogLevel.Trace,
        [LogSeverity.Warning] = LogLevel.Warning,
    };

    private DiscordSocketClient? client;
    private ILogger<Program>? logger;

    /// <summary>
    /// The main entry point for the application. Starts up an asynchronous pipeline.
    /// </summary>
    /// <param name="args">The arguments required at startup.</param>
    /// <returns>The result of the async action.</returns>
    public static Task Main(string[] args)
        => new Program().MainAsync();

    /// <summary>
    /// The asynchronous entry point for the application.
    /// </summary>
    /// <returns>The result of the async action.</returns>
    public async Task MainAsync()
    {
        this.logger = LoggerFactory.Create(builder => builder.AddNLog()).CreateLogger<Program>();
        this.logger.LogInformation("Inactivity bot is starting up.");
        this.client = new DiscordSocketClient();
        this.client.Log += this.Log;

        var codebase = Assembly.GetExecutingAssembly()?.Location ?? throw new InvalidOperationException("Could not determine the location of the assembly.");
        var appSettings = Path.Join(Path.GetDirectoryName(codebase), "appsettings.json");
        var settings = JsonSerializer.Deserialize<ApplicationSettings>(await File.ReadAllTextAsync(appSettings)) ?? throw new InvalidOperationException("Could not deserialize the application settings.");

        var mongoManager = new MongoManager(settings);
        var result = mongoManager.Find(nameof(Token), typeof(Token), "{}");

        var token = string.Empty;

        switch (result)
        {
            case List<Token> tokens:
                token = tokens.FirstOrDefault()?.DiscordToken ?? throw new InvalidOperationException("Could not find a token in the database.");
                break;
            case RequestReport report:
                this.logger.LogError(Encoding.UTF8.GetString(JsonSerializer.SerializeToUtf8Bytes(report)));
                throw new InvalidOperationException("Could not extract token");
        }

        await this.client.LoginAsync(TokenType.Bot, token);
        await this.client.StartAsync();

        await Task.Delay(-1);
    }

    private Task Log(LogMessage message)
    {
        if (this.logger == null)
        {
            throw new InvalidOperationException("Attempted to log a message before the logger was initialized.");
        }

        this.logger.Log(
            this.logLevels[message.Severity],
            message: message.Message,
            message.Source);

        return Task.CompletedTask;
    }
}
