module File1

[<AbstractClass>]
type InsideBase(name: string, manufacture: string, release_date: int32, price: double, cpu:string, ram: string, image_url: string) =
    member this.Name = name
    member this.Manufacture = manufacture
    member this.Releasedate = releasedate
    member this.Price = price
    member this.CPU = cpu
    member this.Image_url = image_url
    member this.RAM = ram

