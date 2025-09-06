@echo off
dotnet publish --nologo -c Release  /p:PublishProfileFullPath="%cd%\src\Properties\Release.pubxml"
tar -a -cf "%cd%\bin\Publish\sMkTaskManager.zip" -C "%cd%\bin\Publish" *.exe
