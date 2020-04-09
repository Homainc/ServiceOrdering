import React from 'react';
import { Link } from 'react-router-dom';
import { Pagination, PaginationItem, PaginationLink } from 'reactstrap';

export const PaginationBlock = props => {

    const pagesNumbers = [...Array(props.pagesCount).keys()];
    const pagesButtons = pagesNumbers.map(num => 
        <PaginationItem key={num + 1} active={props.pageNumber === num + 1}>
            <PaginationLink tag={Link}  to={`${props.pathPrefix}/page/${num + 1}`}>{num + 1}</PaginationLink>
        </PaginationItem>    
    );
    return(
        <Pagination className="d-flex justify-content-center">
            <PaginationItem disabled={props.pageNumber === 1}>
                <PaginationLink first tag={Link} to={`${props.pathPrefix}/page/1`}/>
            </PaginationItem>
            <PaginationItem disabled={props.pageNumber === 1}>
                <PaginationLink previous tag={Link} to={`${props.pathPrefix}/page/${props.pageNumber - 1}`}/>
            </PaginationItem>
            {pagesButtons}
            <PaginationItem disabled={props.pageNumber === props.pagesCount} >
                <PaginationLink next tag={Link} to={`${props.pathPrefix}/page/${props.pageNumber + 1}`}/>
            </PaginationItem>
            <PaginationItem disabled={props.pageNumber === props.pagesCount}>
                <PaginationLink last tag={Link} to={`${props.pathPrefix}/page/${props.pagesCount}`}/>
            </PaginationItem>
        </Pagination>
    );
};