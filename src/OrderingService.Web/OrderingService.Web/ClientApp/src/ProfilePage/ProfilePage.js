import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Card, ListGroupItem, ListGroupItemHeading, ListGroupItemText } from 'reactstrap';
import { profileActions } from '../actions';
import { LoadingContainer, UserPersonalBlock, UserEmployeeBlock } from '../components';

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
                <ListGroupItem>
                    <ListGroupItemHeading>Email</ListGroupItemHeading>
                    <ListGroupItemText className="text-secondary">{profile && profile.email}</ListGroupItemText>
                    <ListGroupItemHeading>Password</ListGroupItemHeading>
                   <ListGroupItemText className="text-secondary">*************</ListGroupItemText>
                </ListGroupItem>
                <UserEmployeeBlock profile={profile}/>
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