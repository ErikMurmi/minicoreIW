import '../styles.css';
export default function Contrato ({contrato}){
    return(
        <div className='contrato-container'>
            <h4>
                {contrato.Nombre}
            </h4>
            <p>Fecha: {contrato.Fecha.toLocaleDateString()}</p>
            <p>Monto: {contrato.Monto}</p>
            <p>IdCliente: {contrato.IdCliente}</p>
        </div>
    )
}