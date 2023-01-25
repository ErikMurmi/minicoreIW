export const getClientes = async()=>{
    const res = await fetch(`https://localhost:7173/Clientes`, {rejectUnauthorized:false})
    const data = await res.json()
    console.log('Clientes', data)
    return data   
}