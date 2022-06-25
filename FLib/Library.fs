namespace FLib

open System.IO
open Newtonsoft.Json.Linq

module Subject =
    type Price = {value:int; currency:string}
    type CPU = {name:string; freq:double; order:int}
    type RAM_Arg = {value:int; order:int}
    type RAM = {min:RAM_Arg; max:RAM_Arg}

    [<AbstractClass>]
    type BaseItem(name: string, image_url: string, manufacturer: string, release_date: int, price: Option<Price>, cpu:Option<CPU>, ram: Option<RAM>) =
        member this.Name = name
        member this.Image_url = image_url
        member this.Manufacture = manufacturer
        member this.Releasedate = release_date
        member this.Price = price
        member this.CPU = cpu
        member this.RAM = ram

    type PC(name: string, image_url: string, manufacturer: string, release_date: int, price: Option<Price>, cpu:Option<CPU>, ram: Option<RAM>, os: string list) =
    inherit BaseItem(name, image_url, manufacturer, release_date, price, cpu, ram)
    member this.OS = os

module JSON =
    type Reader(file:string) =
        member this.items:(Subject.BaseItem list)=
            let rootObj = JObject.Parse(File.ReadAllText(file))
            let obj1 = rootObj["PC"].ToObject<Subject.PC list>()
            let obj2 = rootObj["Console"].ToObject<Subject.Console list>()
            let obj3 = rootObj["PortaConsole"].ToObject<Subject.PortaConsole list>()

            List.map (fun r -> upcast r) obj1 @ List.map (fun r -> upcast r)obj2 @ List.map (fun r -> upcast r) obj3
