import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Col, Row, Card, CardTitle, ListGroup, ListGroupItem, ListGroupItemHeading, ListGroupItemText } from 'reactstrap';
import { profileActions } from '../actions';
import { LoadingContainer, UserPersonalBlock, UserEmployeeBlock } from '../components';

class ProfilePage extends Component {

    componentDidMount(){
        this.props.loadProfile();
    }

    render(){
        const { profile, profileLoading } = this.props;
        return (
            <Row>
                <Col>
                    <Card body className="bg-secondary">
                        <LoadingContainer isLoading={!!profileLoading}>
                            <CardTitle><h5 className="text-light">Profile</h5></CardTitle>
                            <ListGroup>
                                <UserPersonalBlock profile={profile}/>
                                <ListGroupItem>
                                    <ListGroupItemHeading>Email</ListGroupItemHeading>
                                    <ListGroupItemText className="text-secondary">{profile && profile.email}</ListGroupItemText>
                                    <ListGroupItemHeading>Password</ListGroupItemHeading>
                                    <ListGroupItemText className="text-secondary">*************</ListGroupItemText>
                                </ListGroupItem>
                                <UserEmployeeBlock profile={profile}/>
                            </ListGroup>
                        </LoadingContainer>
                    </Card>
                </Col>
            </Row>
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