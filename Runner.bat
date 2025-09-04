@echo off
REM ==============================
REM Run xUnit Tests - ProductAPIAutomation
REM ==============================

echo Running API Automation Tests...



for /f "tokens=2 delims==" %%I in ('"wmic os get localdatetime /value"') do set datetime=%%I
set fileName=TestResults_%datetime:~0,4%_%datetime:~4,2%_%datetime:~6,2%_%datetime:~8,2%_%datetime:~10,2%_%datetime:~12,2%.trx

dotnet test ProductAPIAutomation.sln --logger "trx;LogFileName=TestResults\%fileName%" --results-directory TestResults /p:CollectCoverage=true /p:CoverletOutput=TestResults\coverage.json

echo Tests completed. Results saved in %fileName%.
