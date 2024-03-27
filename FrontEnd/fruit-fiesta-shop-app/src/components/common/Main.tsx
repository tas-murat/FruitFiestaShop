
import { Box } from '@chakra-ui/react';
import { Outlet } from 'react-router-dom';

const Main = () => {
    return (
        <Box as="main" pt="20" pb="20" px="4">
            <Outlet />
        </Box>
    );
};

export default Main;