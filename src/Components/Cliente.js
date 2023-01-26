import '../styles.css';
export default function Cliente ({cliente,color}){

    //console.log('Color:',color)

    return(
        <div style={{backgroundColor:`${color}`,width:'50%',padding:'40px 0 40px 0'}}>
            <h4>
                {cliente.Nombre}
            </h4>
            <p>Total: {cliente.Total}$</p>
        </div>
    )
}