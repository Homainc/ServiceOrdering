import React from 'react';
import { EmployeeItem } from './EmployeeItem';
import { Card } from 'reactstrap';

export const EmployeeList = props => {
    const list = props.employees && props.employees.map(employee => 
        <EmployeeItem key={employee.id} employee={employee}/>
    );

    return(
        <Card>
            {list}
        </Card>
    );
}; 