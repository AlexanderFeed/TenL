namespace FLib

open System.IO
open Newtonsoft.Json.Linq

module Subject =
    type Price = {value:int; currency:string}
    type CPU = {name:string; freq:double; order:int}
    type Memory = {value:int; order:int}
    type RAM = {min:Memory; max:Memory}
    type Battery = {battery_type:string; quantity:int}
    type Screen(colors:int, resolution:string) =
        member this.colors = colors
        member this.resolution = resolution
        member this.GetScreenSquare() =
            let size = this.resolution.Split('x')
            (size[0] |> int) * (size[1] |> int)

    [<AbstractClass>]
    type BaseItem(name: string, image_url: string, manufacturer: string, release_date: int, price: Option<Price>, cpu:Option<CPU>, ram: Option<RAM>) =
        member this.Name = name
        member this.Image_url = image_url
        member this.Manufacturer = manufacturer
        member this.Release_date = release_date
        member this.Price = price
        member this.CPU = cpu
        member this.RAM = ram

    type PC(name: string, image_url: string, manufacturer: string, release_date: int, price: Option<Price>, cpu:Option<CPU>, ram: Option<RAM>, os: string list) =
        inherit BaseItem(name, image_url, manufacturer, release_date, price, cpu, ram)
        member this.OS = os

    type Console(name: string, image_url: string, manufacture: string, release_date: int, price: Option<Price>, cpu:Option<CPU>, ram: Option<RAM>, generation: int, vram: Option<Memory>) =
        inherit BaseItem(name, image_url, manufacture, release_date, price, cpu, ram)
        member this.Generation = generation
        member this.VRAM = vram

    type PortableConsole(name: string, image_url: string, manufacture: string, release_date: int, price: Option<Price>, cpu:Option<CPU>, ram: Option<RAM>, generation: int, vram: Option<Memory>, power_supply: Option<Battery>, screen: Screen) =
        inherit Console(name, image_url, manufacture, release_date, price, cpu, ram, generation, vram)
        member this.Screen = screen
        member this.Power_supply = power_supply

module JSON =
    type Reader(file:string) =
        member this.GetPCs() =
            List.filter (fun (item: Subject.BaseItem) -> item :? Subject.PC) this.items

        member this.GetConsoles() =
            List.filter (fun (item: Subject.BaseItem) -> item :? Subject.Console) this.items

        member this.GetPortables() =
            List.filter (fun (item: Subject.BaseItem) -> item :? Subject.PortableConsole) this.items

        member this.GetBiggestScreenPortable() =
            let portables = List.map(fun (item:Subject.BaseItem) -> item :?> Subject.PortableConsole) (this.GetPortables())
            List.maxBy(fun (item:Subject.PortableConsole)-> item.Screen.GetScreenSquare() ) (portables)

        member this.GetBiggestRAMDevice() =
            List.maxBy(fun (item:Subject.BaseItem)-> item.RAM) (this.items)

        member this.GetEarliestCommodore() =
            let commodores = List.filter (fun (item: Subject.BaseItem) -> item.Manufacturer = "Commodore") this.items
            List.minBy(fun (item:Subject.BaseItem)-> item.Release_date) (commodores)

        member this.GetBiggestVRAMConsole() =
            let consoles = List.map(fun (item:Subject.BaseItem) -> item :?> Subject.Console) (this.GetConsoles())
            List.maxBy(fun (item:Subject.Console)-> item.VRAM) (consoles)

        member this.GetMostOSPC() =
            let pcs = List.map(fun (item:Subject.BaseItem) -> item :?> Subject.PC) (this.GetPCs())
            List.maxBy(fun (item:Subject.PC)-> item.OS.Length) (pcs)

        member this.items:(Subject.BaseItem list)=
            let rootObj = JObject.Parse(File.ReadAllText(file))
            let pc = rootObj["PC"].ToObject<Subject.PC list>()
            let consoles = rootObj["Console"].ToObject<Subject.Console list>()
            let portables = rootObj["PortaConsole"].ToObject<Subject.PortableConsole list>()

            List.map (fun r -> upcast r) pc @ List.map (fun r -> upcast r) consoles @ List.map (fun r -> upcast r) portables
