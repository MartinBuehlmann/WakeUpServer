@ECHO OFF
cd ../source

REM Angular
cd frontend
call npm install
cd ..

REM .NET
cd backend
dotnet restore

pause