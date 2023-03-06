C#言語を使用して、エラー処理のロギングを行う例です。この例では、ログをファイル出力するために、Microsoft.Extensions.Logging.Fileパッケージを使用しています。



例では、Microsoft.Extensions.LoggingとMicrosoft.Extensions.Logging.Fileを使用して、ロガーを作成します。AddFileメソッドを使用して、ログファイルの場所を指定します。isJsonパラメータは、ログファイルをJSON形式で出力するかどうかを指定します。SetMinimumLevelメソッドを使用して、出力するログの最小レベルを指定します。例では、エラーレベル以上のログのみが出力されます。

try-catchブロック内で、logger.LogErrorメソッドを使用して、エラーログを出力します。第一引数には、例外オブジェクトを渡し、第二引数には、エラーログに表示するメッセージを指定します。

上記の例では、ログファイルは、"logs/app-{Date}.txt"のフォーマットで出力されます。{Date}は、ログの出力日付を表します。ログファイルは、アプリケーションの起動時に作成されます。また、Microsoft.Extensions.Logging.Consoleパッケージを使用することで、コンソールにログを出力することもできます。
