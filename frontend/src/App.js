import {useEffect,useState} from "react";
import "./App.css"
function App() {
    const[weather,setWeather] = useState([]);
    useEffect(() => {
        fetch("/weatherforecast")
        .then(res=>res.json())
        .then(data=>setWeather(data))
            .catch((err)=>console.log("API fetch error",err));
        },[]);
    return(
        <div className="App">
            <header className="App-header">
                <h2>TODO app</h2>
                {weather.length===0?(
                    <p>Loading...</p>
                ):(
                    <ul style={{textAlign:"left"}}>
                        {weather.map((item,index)=>(
                            <li key={index}>
                                <strong>{item.date}</strong> - {item.summary},{item.temperature}
                            </li>
                        ))}
                    </ul>
                )}
            </header>
        </div>
    );
}
export default App;