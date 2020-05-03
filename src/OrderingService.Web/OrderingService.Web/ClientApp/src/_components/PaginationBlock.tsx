import React from 'react';
import { Link, useLocation } from 'react-router-dom';
import { Pagination, PaginationItem, PaginationLink } from 'reactstrap';

type PaginationBlockProps = Readonly<{
    pagesCount: number;
    pageNumber: number;
    pathPrefix: string;
}>;

export const PaginationBlock = (props: PaginationBlockProps) => {
    const queryParams = useLocation().search;

    const buildURI = (page: number): string =>
        `${props.pathPrefix}/page/${page}${queryParams}`;

    const pagesNumbers = [...Array(props.pagesCount).keys()];
    const pagesButtons = pagesNumbers.map(num => 
        <PaginationItem key={num + 1} active={props.pageNumber === num + 1}>
            <PaginationLink tag={Link}  to={buildURI(num + 1)}>{num + 1}</PaginationLink>
        </PaginationItem>    
    );
    return(
        <Pagination className="d-flex justify-content-center">
            <PaginationItem disabled={props.pageNumber === 1}>
                <PaginationLink first tag={Link} to={buildURI(1)}/>
            </PaginationItem>
            <PaginationItem disabled={props.pageNumber === 1}>
                <PaginationLink previous tag={Link} to={buildURI(props.pageNumber - 1)}/>
            </PaginationItem>
            {pagesButtons}
            <PaginationItem disabled={props.pageNumber === props.pagesCount} >
                <PaginationLink next tag={Link} to={buildURI(props.pageNumber + 1)}/>
            </PaginationItem>
            <PaginationItem disabled={props.pageNumber === props.pagesCount}>
                <PaginationLink last tag={Link} to={buildURI(props.pagesCount)}/>
            </PaginationItem>
        </Pagination>
    );
};