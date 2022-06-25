module Console

type InsideBase(name: string, manufacture: string, release_date: int32, price: double, cpu:string, ram: string, generation: string, vram: string,image_url: string) =
    inherit FBase(name, manufacture, release_date, price, cpu, ram, generation, vram,image_url)
    member this.Generation = generation
    member this.VRAM = vram