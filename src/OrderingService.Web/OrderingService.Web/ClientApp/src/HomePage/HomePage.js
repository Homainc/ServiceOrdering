import React, { Component } from 'react';
import { EmployeeList } from '../components';
import { employeeActions } from '../actions';
import { connect } from 'react-redux';

class HomePage extends Component {

    componentDidMount(){
        this.props.loadEmployees();    
    }
    render() {
        return (
            <EmployeeList employees={this.props.employeeList}/>
        );
    }
}

const mapStateToProps = state => {
    const { employeeList } = state.employee;
    return {
        employeeList
    };
};

const mapDispatchToProps = dispatch => ({
    loadEmployees: () => dispatch(employeeActions.loadEmployees())
});

const connectedHomePage = connect(mapStateToProps, mapDispatchToProps)(HomePage);
export { connectedHomePage as HomePage };