module DynamicConsole
    open System

    let testShow() =
        printfn "test" 

    let testAdd() =
        printfn "%i" (3 + 10)

    let testChoices  = 
        [1, "Show", testShow
         2, "Add", testAdd ]

    let displayChoices (testChoices: (int * string * (unit -> unit))list) =
        testChoices
        |> Seq.iter(fun (num, str,_) -> printfn "%i. %s" num str)
        printfn "Enter a choice"

    let rec getUserChoice (testChoices: (int * string * (unit -> unit))list) = 
        let input = int (Console.ReadKey(true).KeyChar.ToString())
        testChoices
        |> List.iter(fun (i,_,f) -> 
            if i = input then
                f() )

    let flow testChoices=
        displayChoices testChoices
        getUserChoice testChoices

    flow testChoices
    // let test (a: list<string * Func<_>>) =
    //     a

    // let showChoices choices =
    //     choices
    //     |> Seq.fold(fun i s -> printfn "%i. %s" i s; i+1) 1

    // let showChoices2 choices=
    //     choices
    //     |> Seq.iter(fun (num, str)-> printfn "%i. %s" num str)


    // Dynamic Example
