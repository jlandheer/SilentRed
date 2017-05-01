rd /s /q bin
rd /s /q obj
for /f "delims=" %%x in ('dir /b /ad /s bin') do rd /s /q "%%x"
for /f "delims=" %%x in ('dir /b /ad /s obj') do rd /s /q "%%x"
rd /s /q "%USERPROFILE%\AppData\Local\Temp\Temporary ASP.NET Files\root"
rd /s /q "%USERPROFILE%\AppData\Local\Temp\Temporary ASP.NET Files\vs"