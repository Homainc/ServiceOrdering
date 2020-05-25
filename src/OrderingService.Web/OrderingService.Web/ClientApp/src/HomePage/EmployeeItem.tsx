import React from 'react';
import { ListGroupItem, Col, CardImg } from 'reactstrap';
import { Link } from 'react-router-dom';
import { EmployeeProfileDto } from '../WebApiModels';
import { Rating } from '../_components';
import '@fortawesome/fontawesome-free/css/all.css';
import './employee-item.scss';
import { cloudinary } from '../_helpers';

type EmployeeItemProps = Readonly<{
    employee: EmployeeProfileDto;
}>;

export const EmployeeItem = (props: EmployeeItemProps) => {
    const { user } = props.employee;
    return(
        <ListGroupItem className="d-flex flex-row py-4 px-0 align-items-center justify-content-center">
            <CardImg src={cloudinary.url(user?.imagePublicId || 'default-employee', { height: 130, width: 130, crop: 'scale' })} 
                    className='col-2 p-0 ml-3 rounded-circle employee-img' 
                    style={{ objectFit: 'cover', width: '100%' }}
                    alt={`${user?.firstName} avatar`}/>
            <Col lg="7" sm='6' xs='6' className='d-flex flex-column justify-content-center text-dark text-decoration-none pl-5' tag={Link} to={`/employee/${props.employee.id}`}>
                <h1 className='employee-h1'>{user?.firstName} {user?.lastName}</h1>
                <h2 className='employee-h2 text-secondary'>{props.employee.serviceType}</h2>
            </Col>
            <Col lg='1' sm='1' xs='1' className='d-flex flex-column justify-content-center collapse-xs'>
                <Link to={`/order/${props.employee.id}`}>
                    <h3 className='employee-h3'><i className="fas fa-user-plus font-size-2"/></h3>Hire
                </Link>
            </Col>
            <Col lg='2' sm='3' xs='4' className='d-flex flex-column justify-content-center'>
                <h1 className='employee-h2'>$ {props.employee.serviceCost.toFixed(2)}</h1>
                <Rating rate={props.employee.averageRate}/>
            </Col>
        </ListGroupItem>
    );
};