﻿C#言語を使用して、エラー処理のロギングを行う例です。この例では、ログをファイル出力するために、SeriLogライブラリを使用しています。

SeriLogライブラリを使用して、ロガーを作成しています。WriteToメソッドを使用して、ログファイルの場所を指定します。rollingIntervalパラメータは、ログファイルのローテーション間隔を指定します。例では、1日ごとにログファイルがローテーションされます。

try-catchブロック内で、Log.Errorメソッドを使用して、エラーログを出力します。第一引数には、例外オブジェクトを渡し、第二引数には、エラーログに表示するメッセージを指定します。

上記の例では、ログファイルは、"logs/app-.txt"のフォーマットで出力されます。ファイル名の末尾には、ローテーションされた日付が自動的に付加されます。また、SeriLogライブラリは、多数のログ出力先をサポートしており、コンソールにログを出力することもできます。
