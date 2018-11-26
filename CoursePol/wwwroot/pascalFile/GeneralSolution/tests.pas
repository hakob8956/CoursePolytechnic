unit Tests;

{$mode objfpc}
{$H+}

interface

uses
 TestFramework;

type
 TTestCaseSolution2= class(TTestCase)
 published
   procedure TestResult;
 end;


procedure RegisterTests;

implementation

uses
  SysUtils, Solutions;

procedure RegisterTests;
begin
  TestFramework.RegisterTest(TTestCaseSolution2.Suite);
end;
procedure TTestCaseSolution2.TestResult;
begin

  Check(Solutions.Solution2(5,4,3) = 5, 'Incorrect');
end;
end.
