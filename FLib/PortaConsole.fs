module PortaConsole

type InsideBase(name: string, manufacture: string, release_date: int32, price: double, cpu:string, ram: string, generation: string, vram: string, power_supply: string, screen: int32,image_url: string) =
    inherit FBase(name, manufacture, release_date, price, cpu, ram, generation, vram, power_supply, screen,image_url)
    member this.Screen = screen
    member this.Power_supply = power_supply