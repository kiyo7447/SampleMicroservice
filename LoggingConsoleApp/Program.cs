using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.File;

// ロガーの設定
var loggerFactory = LoggerFactory.Create(builder =>
{
	builder
		.AddFile("logs/app-{Date}.txt", isJson: false)
		.SetMinimumLevel(LogLevel.Error);
});
ILogger logger = loggerFactory.CreateLogger<Program>();

try
{
	// 例外が発生する可能性のある処理
}
catch (Exception ex)
{
	// エラーログの出力
	logger.LogError(ex, "エラーが発生しました");
}
