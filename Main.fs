module fsharp_fscheck

open System.Text.RegularExpressions
open Expecto
open FsCheck

let emailRegex = "[A-Z0-9a-z._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,64}"

let isValidEmail emailToValidate = Regex.IsMatch(emailToValidate, emailRegex)

type EmailGen() =
    static member Email(): Arbitrary<string> =
        // let upper = Gen.elements [ 'A' .. 'Z' ]
        // let lower = Gen.shuffle [ 'a' .. 'z' ]
        Arb.from<string>

let config =
    { FsCheckConfig.defaultConfig with
          maxTest = 100
          arbitrary = [ typeof<EmailGen> ] }

let properties =
    testList "Test email address"
        [
        // you can also override the FsCheck config
        testPropertyWithConfig config "Is a valid email" <| fun a -> Expect.isTrue (isValidEmail a) "True" ]


[<EntryPoint>]
let main argv = Tests.runTests defaultConfig properties
