import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import { Card, ListGroupItemHeading, ListGroupItemText } from 'reactstrap';
import { profileActions } from '../_actions';
import { LoadingContainer } from '../_components';
import { UserEmployeeBlock } from './UserEmployeeBlock';
import { UserPersonalBlock } from './UserPersonalBlock';
import { UserOrdersTable } from './UserOrdersTable';

const ProfilePage = props => { 
    const { profile, profileLoading, loadProfile } = props;

    useEffect(() => {
        loadProfile();
    }, [ loadProfile ]);

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
                <UserOrdersTable userId={profile && profile.id}/>
            </Card>    
        </LoadingContainer>
    );   
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