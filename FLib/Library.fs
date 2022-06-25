namespace FLib

open System.IO
open Newtonsoft.Json.Linq

module Subject =
    [<AbstractClass>]
    type BaseItem(name:string) =
        member this.name = name

module JSON =
    type Reader(file:string) =
        member this.items:(Subject.BaseItem list)=
            let rootObj = JObject.Parse(File.ReadAllText(file))
            let obj1 = rootObj["PC"].ToObject<Subject.PC list>()
            let obj2 = rootObj["Console"].ToObject<Subject.Console list>()
            let obj3 = rootObj["PortaConsole"].ToObject<Subject.PortaConsole list>()

            List.map (fun r -> upcast r) obj1 @ List.map (fun r -> upcast r)obj2 @ List.map (fun r -> upcast r) obj3
