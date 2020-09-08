module DynamicConsole
    open System

    let private displayChoices testChoices =
        testChoices
        |> Seq.iter(fun (char, str, _) -> printfn "%c. %s" char str)
        printfn "Pick a number from the list"
        testChoices

    let rec private getUserChoice g testChoices = 
        let input = Console.ReadKey(true).KeyChar
        testChoices
        |> List.tryFind(fun (i,_,_) -> i = input)
        |> fun o ->
            match o with
            | Some r -> 
                let _, _, f = r
                f g
            | None -> 
                printfn "Invalid entry. Pick a choice from the list"
                getUserChoice g testChoices

    let flow g testChoices=
        displayChoices testChoices
        |> getUserChoice g