import React, { Component } from 'react';

export class DotNetCore extends Component {

    constructor(props) {
        super(props);
        this.state = { dayAnswer: "", loading: true };
    }

    componentDidMount() {
        this.getDayAnswer();
    }

    static renderTextTest(response) {
        return (<p> {response} </p>);
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : DotNetCore.renderTextTest(this.state.dayAnswer);
        return (
            <div>
                <h1>
                    <p>This is Where C# Code will go</p>
                    {contents}
                </h1>
            </div>
        );
    }

    async getDayAnswer() {
        var response = await fetch('dotnetday');
        var data = await response.json();
        this.setState({ dayAnswer: data, loading: false });
    }
}