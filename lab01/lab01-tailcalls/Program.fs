let rec loop n = 
  let s = "hello"
  printfn "%d %s" n s
  loop (n + 1)
  // But if you uncomment the following, it will stack overflow!
  // (The stack will be needed to remember we need to print.)
  //
  // printfn "done"

loop 0
