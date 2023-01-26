import '../styles.css';
export default function Cliente ({cliente,color}){

    //console.log('Color:',color)

    return(
        <div style={{backgroundColor:`${color}`}}>
            <h4>
                {cliente.Nombre}
            </h4>
            <p>Total: {cliente.Total}</p>
        </div>
    )
}