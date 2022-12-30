import { useEffect, useState, useRef } from "react";
import axios from "axios";
import { Person } from "../components/Person";
import Table from "react-bootstrap/Table";
import { Link } from "react-router-dom";

export function People() {
  const [people, setPeople] = useState([]);
  const [sortState, setSortState] = useState("none");
  const { current: RefPeople } = useRef(people);
  const sortMethods = {
    none: { method: (a, b) => null },
    descending: { method: (a, b) => a.personName.localeCompare(b.personName) },
  };

  useEffect(() => {
    axios
      .get("https://localhost:7223/api/react")
      .then((response) => setPeople(response.data));
    document.title = `list of people`;
  }, [RefPeople]);

  if (people.length === 0) {
    return (
      <div className="container">
        <h4>No people found</h4>
      </div>
    );
  }
  return (
    <div className="container people">
      <h1 className="title">List of People</h1>
      <button
        className="btn btn-outline-dark"
        onClick={() => setSortState("descending")}
      >
        Sort People Alpabetically
      </button>
      <br />
      <br />
      <Table>
        <thead>
          <tr>
            <th>Name</th>
            <th>Details</th>
          </tr>
        </thead>
        <tbody>
          {people.sort(sortMethods[sortState].method).map((people) => (
            <Person key={people.id} person={people} />
          ))}
        </tbody>
      </Table>
      <Link to="/CreatePerson">Create New Person</Link>
    </div>
  );
}
