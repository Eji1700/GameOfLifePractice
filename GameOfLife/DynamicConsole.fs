module DynamicConsole
    open System

    let private displayMenu menu =
        Console.SetCursorPosition(0,0)
        menu
        |> Seq.iter(fun (char, str, _) -> printfn "%c. %s" char str)
        printfn "Pick a number from the list"
        menu

    let rec private getUserInputAndExecute g menu = 
        let input = Console.ReadKey(true).KeyChar
        menu
        |> List.tryFind(fun (i,_,_) -> i = input)
        |> fun o ->
            match o with
            | Some r -> 
                let _, _, f = r
                f g
            | None -> 
                printfn "Invalid entry. Pick a choice from the list"
                Console.ReadKey() |> ignore
                Console.Clear()
                displayMenu menu
                |> getUserInputAndExecute g

    let flow g menu =
        displayMenu menu
        |> getUserInputAndExecute g