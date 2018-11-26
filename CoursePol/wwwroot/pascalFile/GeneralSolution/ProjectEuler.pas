program ProjectEuler;

{$mode tp} // Switch fpc to TurboPascal mode
{$H+}      // Use AnsiStrings

uses
 TextTestRunner,
 TestFrameworkProxyIfaces,
 Tests;



var
  testRes: ITestResult;

begin

  Tests.RegisterTests;
  testRes := RunRegisteredTests;

  If testRes.FailureCount <> 0 then Halt(1);
end.

