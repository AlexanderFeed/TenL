namespace FLib

open System.IO
open Newtonsoft.Json.Linq

module Subject =
    let orderString order =
        match order with
        |1 -> "К"
        |2 -> "М"
        |3 -> "Г"
        |_ -> ""

    type Price(value:int, currency:string) =
        member this.value = value
        member this.currency = currency
        member this.GetDesc() =
            if this.value = 0 then ""
            else "Цена: " + this.value.ToString() + this.currency + "\r\n"

    type CPU(name:string, freq:double, order:int) =
        member this.name = name
        member this.freq = freq
        member this.order = order
        member this.GetDesc() =
            "ЦП: " + name + " " + freq.ToString() + (orderString order) + "Гц" + "\r\n"

    type Memory(value:int, order:int) =
        member this.value = value
        member this.order = order
        member this.GetDesc() =
            "Видеопамять: " + value.ToString() + orderString order + "Б" + "\r\n"

    type RAM(min:Memory, max:Memory) =
        member this.min = min
        member this.max = max
        member this.GetDesc() =
            let snd = 
                if min.value = max.value && min.order = max.order then "\r\n"
                else " - " + max.value.ToString() + orderString max.order + "Б" + "\r\n"
            "ОЗУ: " + min.value.ToString() + orderString min.order + "Б" + snd

    type Battery(battery_type:string, quantity:int) =
        member this.battery_type = battery_type
        member this.quantity = quantity
        member this.GetDesc() =
            "Питание: " + quantity.ToString() + " " + battery_type + "\r\n"

    type Screen(colors:int, resolution:string) =
        member this.colors = colors
        member this.resolution = resolution
        member this.GetScreenSquare() =
            let size = this.resolution.Split('x')
            (size[0] |> int) * (size[1] |> int)
        member this.GetDesc() =
            "Разрешение экрана: " + resolution + "\r\nКоличество цветов: " + colors.ToString() + "\r\n"

    [<AbstractClass>]
    type BaseItem(name: string, image_url: string, manufacturer: string, release_date: int, price: Price, cpu:CPU, ram: RAM) =
        member this.Name = name
        member this.Image_url = image_url
        member this.Manufacturer = manufacturer
        member this.Release_date = release_date
        member this.Price = price
        member this.CPU = cpu
        member this.RAM = ram

        abstract member GetDesc : unit -> string

    type PC(name: string, image_url: string, manufacturer: string, release_date: int, price: Price, cpu:CPU, ram: RAM, os: string list) =
        inherit BaseItem(name, image_url, manufacturer, release_date, price, cpu, ram)
        member this.OS = os

        override this.GetDesc() =
            let osDesc = "Доступные ОС: " + (this.OS |> List.fold (fun r s -> if r = "" then s else r + ", " + s) "")
            "Название: " + this.Name + "\r\n" +
            "Производитель: " + this.Manufacturer + "\r\n" +
            "Дата выпуска: " + this.Release_date.ToString() + "г." + "\r\n" +
            this.Price.GetDesc() +
            this.CPU.GetDesc() +
            this.RAM.GetDesc() +
            osDesc

    type Console(name: string, image_url: string, manufacturer: string, release_date: int, price: Price, cpu:CPU, ram: RAM, generation: int, vram: Memory) =
        inherit BaseItem(name, image_url, manufacturer, release_date, price, cpu, ram)
        member this.Generation = generation
        member this.VRAM = vram

        override this.GetDesc() =
            "Название: " + this.Name + "\r\n" +
            "Производитель: " + this.Manufacturer + "\r\n" +
            "Дата выпуска: " + this.Release_date.ToString() + "г." + "\r\n" +
            "Поколение: " + this.Generation.ToString() + "\r\n" +
            this.Price.GetDesc() +
            this.CPU.GetDesc() +
            this.RAM.GetDesc() +
            this.VRAM.GetDesc()

    type PortableConsole(name: string, image_url: string, manufacturer: string, release_date: int, price: Price, cpu:CPU, ram: RAM, generation: int, vram: Memory, power_supply: Battery, screen: Screen) =
        inherit Console(name, image_url, manufacturer, release_date, price, cpu, ram, generation, vram)
        member this.Screen = screen
        member this.Power_supply = power_supply

        override this.GetDesc() =
            "Название: " + this.Name + "\r\n" +
            "Производитель: " + this.Manufacturer + "\r\n" +
            "Дата выпуска: " + this.Release_date.ToString() + "г." + "\r\n" +
            "Поколение: " + this.Generation.ToString() + "\r\n" +
            this.Price.GetDesc() +
            this.CPU.GetDesc() +
            this.RAM.GetDesc() +
            this.VRAM.GetDesc() +
            this.Screen.GetDesc() +
            this.Power_supply.GetDesc()

module JSON =
    type Reader(file:string) =
        member this.items:(Subject.BaseItem list)=
            let rootObj = JObject.Parse(File.ReadAllText(file))
            let pc = rootObj["PC"].ToObject<Subject.PC list>()
            let consoles = rootObj["Consoles"].ToObject<Subject.Console list>()
            let portables = rootObj["Portable Consoles"].ToObject<Subject.PortableConsole list>()

            List.map (fun r -> upcast r) pc @ List.map (fun r -> upcast r) consoles @ List.map (fun r -> upcast r) portables

        member this.GetPCs() =
            List.filter (fun (item: Subject.BaseItem) -> item :? Subject.PC) this.items

        member this.GetConsoles() =
            List.filter (fun (item: Subject.BaseItem) -> (item :? Subject.Console) && not (item :? Subject.PortableConsole)) this.items

        member this.GetPortables() =
            List.filter (fun (item: Subject.BaseItem) -> item :? Subject.PortableConsole) this.items

        member this.GetBiggestScreenPortable() =
            let portables = List.map(fun (item:Subject.BaseItem) -> item :?> Subject.PortableConsole) (this.GetPortables())
            List.maxBy(fun (item:Subject.PortableConsole)-> item.Screen.GetScreenSquare() ) (portables)

        member this.GetBiggestRAMDevice() =
            List.maxBy(fun (item:Subject.BaseItem)-> item.RAM.max.value * ((1024.0 ** item.RAM.max.order) |> int) ) (this.items)

        member this.GetEarliestCommodore() =
            let commodores = List.filter (fun (item: Subject.BaseItem) -> item.Manufacturer = "Commodore") this.items
            List.minBy(fun (item:Subject.BaseItem)-> item.Release_date) (commodores)

        member this.GetBiggestVRAMConsole() =
            let consoles = List.map(fun (item:Subject.BaseItem) -> item :?> Subject.Console) (this.GetConsoles())
            List.maxBy(fun (item:Subject.Console)-> item.VRAM.value*(1024*item.VRAM.order)) (consoles)

        member this.GetMostOSPC() =
            let pcs = List.map(fun (item:Subject.BaseItem) -> item :?> Subject.PC) (this.GetPCs())
            List.maxBy(fun (item:Subject.PC)-> item.OS.Length) (pcs)
