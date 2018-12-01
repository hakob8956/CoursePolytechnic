unit Tests;

{$mode objfpc}
{$H+}

interface

uses
 TestFramework;

type
 TTestCaseSolution1= class(TTestCase)
 published
   procedure TestResult;
 end;


procedure RegisterTests;

implementation

uses
  SysUtils, Solutions;

procedure RegisterTests;
begin
  TestFramework.RegisterTest(TTestCaseSolution1.Suite);
end;
procedure TTestCaseSolution1.TestResult;
begin

  Check(Solutions.Solution1 = 2, 'Incorrect');
end;
end.
