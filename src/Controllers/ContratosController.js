export const getContratos = async()=>{
    const res = await fetch(`https://localhost:7173/Contratos`, {rejectUnauthorized:false})
    const data = await res.json()
    console.log('Contratos', data)
    return data   
}