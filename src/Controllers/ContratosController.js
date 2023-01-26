export const getContratos = async()=>{
    const res = await fetch(`https://api20230125171714.azurewebsites.net/Contratos`, {rejectUnauthorized:false})
    const data = await res.json()
    console.log('Contratos', data)
    return data   
}