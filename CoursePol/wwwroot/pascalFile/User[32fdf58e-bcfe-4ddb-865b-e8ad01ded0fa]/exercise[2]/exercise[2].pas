function Solution2(a,b,c: Int64): Int64;
implementation
   function Solution2(a,b,c: Int64): Int64;
   var max:Int64;
   begin
      if a>b then max:=a else max:=b;
      if max < c then max:=c
      Solution2:=max;
   end;
