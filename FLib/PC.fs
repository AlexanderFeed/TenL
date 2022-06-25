module PC

type PC(name: string, manufacture: string, release_date: int32, price: double, cpu:string, ram: string, os: string,image_url: string) =
    inherit FBase(name, manufacture, release_date, price, cpu, ram, os,image_url)
    member this.OS = os