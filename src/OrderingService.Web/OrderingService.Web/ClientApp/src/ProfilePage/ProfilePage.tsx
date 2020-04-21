import React, { useEffect } from 'react';
import { connect, ConnectedProps } from 'react-redux';
import { Card, ListGroupItemHeading, ListGroupItemText } from 'reactstrap';
import { LoadingContainer } from '../_components';
import { UserEmployeeBlock } from './UserEmployeeBlock';
import { UserPersonalBlock } from './UserPersonalBlock';
import { RootState } from '../_store';
import { ThunkDispatch } from 'redux-thunk';
import { ProfileActionTypes, ProfileState } from '../_store/profile/types';
import * as profileActions from '../_store/profile/actions';

const mapState = (state: RootState) => ({
    profileLoading: state.profile.loading,
    profile: state.profile.profile
});

const mapDispatch = (
    dispatch: ThunkDispatch<ProfileState, undefined, ProfileActionTypes>
) => ({
    loadProfile: () => dispatch(profileActions.load())
});

const connector = connect(mapState, mapDispatch);
type PropsFromRedux = ConnectedProps<typeof connector>;
type ProfilePageProps = PropsFromRedux & Readonly<{}>;

const ProfilePage = (props: ProfilePageProps) => { 
    const { profile, profileLoading, loadProfile } = props;

    useEffect(() => {
        loadProfile();
    }, [ loadProfile ]);

    return (
        <LoadingContainer isLoading={!!profileLoading}>
            <Card body className="bg-light">
                <UserPersonalBlock/>
                <hr/>
                <ListGroupItemHeading>Email</ListGroupItemHeading>
                <ListGroupItemText className="text-secondary">{profile && profile.email}</ListGroupItemText>
                <ListGroupItemHeading>Password</ListGroupItemHeading>
                <ListGroupItemText className="text-secondary">*************</ListGroupItemText>
                <hr/>
                <UserEmployeeBlock/>
            </Card>    
        </LoadingContainer>
    );   
}

const connectedProfilePage = connector(ProfilePage);
export { connectedProfilePage as ProfilePage };