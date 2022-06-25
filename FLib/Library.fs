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
            let objs = rootObj["Items"].ToObject<Subject.BaseItem list>()

            List.map (fun r -> upcast r) objs