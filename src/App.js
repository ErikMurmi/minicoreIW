import './App.css';
import { useState, useEffect} from 'react';
import { Contratos } from './data';
import Cliente from './Components/Cliente';
import Contrato from './Components/Contrato';
import { getContratos } from './Controllers/ContratosController';
import { getTotales } from './Controllers/ClientesController';

function App() {

  const [rango,setRango] = useState(
  {
    FechaInicio:"",
    FechaFin:""
  })

  const [contratos,setContratos] = useState(Contratos) 

  const [clientes,setClientes] = useState(
    {
      UDLA:0,
      SUPERMAXI:0,
      CIGARRA:0,
      WHATEVER:0
    }
  )

  const [totales,setTotales] = useState([])


  useEffect(()=>{
    if(rango.FechaInicio && rango.FechaFin){
      console.log('Rango: ',rango)
      calculateContratos()
    }
  },[rango])

  const calculateContratos=async()=>{
    setTotales(await getTotales(rango.FechaInicio,rango.FechaFin))
  }

  const handleChange = (e) => {
    const { value, name } = e.target
    setRango({ ...rango, [name]: value})
    console.log('Nuevo rango ', rango)
  }

  function randomHexColor() {
    return '#' + Math.floor(Math.random()*16777215).toString(16);
  }


  return (
    <div className="App">
      <h1>Mini-Core</h1>
      <p>Escoge el rango de fecha</p>
      <button onClick={calculateContratos}>Traer info</button>
        <form>
          <div style={{display:"flex",flexDirection:"column",width:"fit-content"}}>
            <label>Fecha Inicio</label>
            <input name="FechaInicio" type="date" onChange={handleChange} />
          </div>
          <div style={{display:"flex",flexDirection:"column",width:"fit-content"}}>
            <label>Fecha Fin</label>
            <input name="FechaFin" type="date" onChange={handleChange}/>
          </div>
        </form>
      <div className='page-container'>
        <div className='section'>
          <div style={{overflowY:"visible",borderColor:"red"}}>
          {
            // contratos.map(contrato=>console.log(contrato.Nombre))
            contratos.map(ctr=>
            <>
            <Contrato contrato={ctr}/>
            </>)
          }
          </div>
        </div>
        <div className='section' id='clientes'>
        {totales.map((total) =><>
          <Cliente cliente={total} color={randomHexColor()}/>
        </>)
        }
        </div>
        
      </div>
    </div>
  );
}

export default App;
