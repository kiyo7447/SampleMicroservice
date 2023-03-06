using Serilog;

// ロガーの設定
Log.Logger = new LoggerConfiguration()
	.WriteTo.File("logs/app-.txt", rollingInterval: RollingInterval.Day)
	.CreateLogger();

try
{
	// 例外が発生する可能性のある処理
}
catch (Exception ex)
{
	// エラーログの出力
	Log.Error(ex, "エラーが発生しました");
}
