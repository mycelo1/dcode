#!/bin/sh

rm -rf ./publish/
dotnet publish -o publish -r win-x64 -c Release -p:PublishTrimmed=true -p:PublishSingleFile=true -p:PublishReadyToRun=true
