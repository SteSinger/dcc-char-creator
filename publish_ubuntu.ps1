rmdir -r .\dcc
rm dcc.zip
dotnet publish -c Release -r ubuntu.18.04-x64 .\DccCharCreator.web\DccCharCreator.web.csproj -o dcc
Compress-Archive .\dcc dcc.zip
