import { Link } from "react-router-dom";

export function Person(props) {
  const person = props.person;

  return (
    <tr>
      <td>
        <p>{person.personName}</p>
      </td>
      <td>
          <Link to={`/${person.id}`} key={person.id}>
          View Details
        </Link>
      </td>
    </tr>
  );
}
