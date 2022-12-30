import axios from "axios";
import React from "react";
// import Multiselect from "multiselect-react-dropdown";
import ReactFormInputValidation from "react-form-input-validation";
import { Navigate } from "react-router-dom";

export class CreatePerson extends React.Component {
  constructor(props) {
    super(props);
    //#region
    this.state = {
      fields: {
        name: "",
      },

      cityId: "",
      countryId: "",
      languageId: "",

      allCountries: [],
      allCities: [],
      allLanguages: [],

      errors: {},

      personCreated: false,
    };
    //#endregion

    this.form = new ReactFormInputValidation(this);
    this.form.useRules({
      name: "required",
    });

    // Fillin Countries and languages dropdown lists.
    this.componentDidMount = () => {
      axios
        .get("https://localhost:7223/api/react/countries")
        .then((response) => this.setState({ allCountries: response.data }));

      axios
        .get("https://localhost:7223/api/react/languages")
        .then((response) => this.setState({ allLanguages: response.data }));
    };

    this.form.onformsubmit = () => {
      if (!this.state.errors.name) {
        const person = {
          name: this.state.fields.name,
          cityId: this.state.cityId,
          languageId: this.state.languageId,
        };

        fetch("https://localhost:7223/api/react/create/", {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify(person),
        })
          .then((response) => response.status)
          .then((response) =>
            response === 201
              ? this.setState({ personCreated: true })
              : alert("Please fill in correct info.")
          )
          .then(this.forceUpdate());
      }
    };

    this.setCountry = (e) => {
      this.setState({ countryId: e.target.value });
    };

    this.setLanguage = (e) => {
      this.setState({ languageId: e.target.value });
    };

    this.fetchCities = (e) => {
      axios
        .get("https://localhost:7223/api/react/cities/" + e)
        .then((response) => this.setState({ allCities: response.data }));
      console.log(e);
    };
  }
  render() {
    const { personCreated } = this.state;

    if (personCreated) {
      return <Navigate to="/People" />;
    }
    return (
      <div className="container add-person mt-5">
        <form onSubmit={this.form.handleSubmit}>
          <input
            type="text"
            name="name"
            placeholder="Name"
            value={this.state.fields.name}
            onChange={this.form.handleChangeEvent}
          />
          <label className="error">
            {this.state.errors.name ? this.state.errors.name : ""}
          </label>{" "}
          <br />
          <select
            name="country"
            defaultValue={"default"}
            onChange={(e) => {
              this.setCountry(e);
              console.log(e);
              this.fetchCities(e.target.value);
            }}
          >
            <option disabled value="default">
              Select Country
            </option>
            {this.state.allCountries.map((country) => {
              return (
                <option key={country.id} value={country.id}>
                  {country.countryName}
                </option>
              );
            })}
          </select>
          <br />
          <select
            name="city"
            defaultValue={"default"}
            onChange={(e) => this.setState({ cityId: e.target.value })}
          >
            <option disabled value="default">
              Select City
            </option>
            {this.state.countryId === "" ? (
              <option className="italic" disabled value="default">
                Select Country First
              </option>
            ) : (
              <></>
            )}
            {this.state.allCities.map((city) => {
              return (
                <option key={city.id} value={city.id}>
                  {city.cityName}
                </option>
              );
            })}
          </select>
          <br />
          <select
            name="language"
            defaultValue={"default"}
            onChange={(e) => {
              this.setLanguage(e);
              console.log(e);
            }}
          >
            <option disabled value="default">
              Select a Language
            </option>
            {this.state.allLanguages.map((language) => {
              return (
                <option key={language.id} value={language.id}>
                  {language.languageName}
                </option>
              );
            })}
          </select>
          <br />
          <button type="submit" className="btn btn-outline-dark">
            Submit
          </button>
        </form>
      </div>
    );
  }
}
