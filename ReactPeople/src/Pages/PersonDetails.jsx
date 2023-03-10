import { useLocation, useNavigate } from "react-router-dom";
import { Table } from "react-bootstrap";
import axios from "axios";
import { useState, useEffect } from "react";
import { EditPerson } from "../components/EditPerson";

export function PersonDetails() {
  const location = useLocation();
  const id = location.pathname;
  console.log(id);

  const navigate = useNavigate();
  const [details, setDetails] = useState();
  const [editPerson, setEditPerson] = useState(false);

  useEffect(() => {
    fetchDetails(id);
  }, []);

  async function fetchDetails(id) {
    await axios
      .get("https://localhost:7223/api/react" + id)
      .then((response) => setDetails(response.data));
    console.log("fetchDetails", details);
  }

  async function DeletePerson() {
    await axios
      .delete("https://localhost:7223/api/React" + id)
      .then((response) => (response.status === 200 ? navigate("/People") : ""));
  }

  while (details == null) {
    console.log("empty Details", details);
    return <></>;
  }
  return (
    <div className="container m-5">
      <Table bordered className="details-table">
        <tbody>
          <tr>
            <th>Name</th>
            <td>{details.name}</td>
          </tr>
          <tr>
            <th>City</th>
            <td>{details.city}</td>
          </tr>
          <tr>
            <th>Country</th>
            <td>{details.country}</td>
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
      <button
        className="btn btn-outline-dark"
        onClick={() => setEditPerson(true)}
      >
        Edit Person
      </button>
      <button
        className="btn btn-outline-dark m-1"
        onClick={() => DeletePerson()}
      >
        Delete Person
      </button>

      {editPerson === true ? (
        <EditPerson person={"person"} details={details} />
      ) : (
        <></>
      )}
    </div>
  );
}
