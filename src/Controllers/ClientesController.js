export const getClientes = async()=>{
    const res = await fetch(`https://api20230125171714.azurewebsites.net/Clientes`, {rejectUnauthorized:false})
    const data = await res.json()
    console.log('Clientes', data)
    return data   
}

export const getTotales = async(inicio,fin)=>{
    const res = await fetch(`https://api20230125171714.azurewebsites.net/Clientes/GetTotales?inicio=${inicio}&fin=${fin}`, {rejectUnauthorized:false})
    const data = await res.json()
    console.log('Totales', data)
    return data   
}