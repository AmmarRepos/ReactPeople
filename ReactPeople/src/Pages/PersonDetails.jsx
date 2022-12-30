import { useLocation, useNavigate} from "react-router-dom";
import { Table } from "react-bootstrap";
import axios from "axios";
import {useState, useEffect } from "react";
import { EditPerson } from "../components/EditPerson";

export function PersonDetails (){

    const location = useLocation();
    console.log(location.pathname);
    const id = location.pathname;
   
    const navigate = useNavigate();
    const [details, setDetails] = useState();
    const [editPerson, setEditPerson] = useState(false);

    useEffect(() => {
        axios.get("https://localhost:7223/api/react" + id)
            .then(response => setDetails(response.data))
	console.log(details);
    },[]);
    
    async function DeletePerson(){
        await axios.delete('https://localhost:7223/api/React/' + id)
            .then(response => response.status === 202 ? navigate("/People") : "")
        
    }

    while(details == null)
    {
       return(
        <></>
       )
    }
    return(
        <div className="container m-5">
        <Table bordered className="details-table">
            <tbody>
                <tr>
                    <th>Name</th>
                    <td>{details.personName}</td>
                </tr>
                <tr>
                    <th>City</th>
                 
                    <td>{details.city.cityName}</td>
                </tr>
                <tr>
                    <th>Country</th>
                    <td>{details.city.country.countryName}</td>
                </tr>
                <tr>
                    <th>Languages</th>
                    <td>
                        {details.languages.map((language) => (  
                            <p>{language}</p>
                        ))}
                    </td>
                </tr>
            </tbody>
        </Table>

        <button className="btn btn-outline-dark" onClick={() => setEditPerson(true)} >Edit Person</button>                    
        <button className="btn btn-outline-dark m-1" onClick={() => DeletePerson()} >Delete Person</button>

        {editPerson === true ? <EditPerson person={"person"} details={details}/> : <></>}

        </div>
    )
    
}
