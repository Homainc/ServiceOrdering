import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Card, ListGroupItemHeading, ListGroupItemText } from 'reactstrap';
import { profileActions } from '../actions';
import { LoadingContainer, UserPersonalBlock, UserEmployeeBlock, OrdersTable } from '../components';

class ProfilePage extends Component {

    componentDidMount(){
        this.props.loadProfile();
    }

    render(){
        const { profile, profileLoading } = this.props;
        return (
            <LoadingContainer isLoading={!!profileLoading}>
                <Card body className="bg-light">
                    <UserPersonalBlock profile={profile}/>
                    <hr/>
                    <ListGroupItemHeading>Email</ListGroupItemHeading>
                    <ListGroupItemText className="text-secondary">{profile && profile.email}</ListGroupItemText>
                    <ListGroupItemHeading>Password</ListGroupItemHeading>
                    <ListGroupItemText className="text-secondary">*************</ListGroupItemText>
                    <hr/>
                    <UserEmployeeBlock profile={profile}/>
                    <hr/>
                    <h5>My orders</h5>
                    <OrdersTable userId={profile && profile.id}/>
                </Card>    
            </LoadingContainer>
        );   
    }
}

const mapStateToProps = state => {
    const { profileLoading, profile } = state.profile;
    return {
        profileLoading,
        profile
    };
};

const mapDispatchToProps = dispatch => ({
    loadProfile: () => dispatch(profileActions.loadProfile())
});

const connectedProfilePage = connect(mapStateToProps, mapDispatchToProps)(ProfilePage);
export { connectedProfilePage as ProfilePage };