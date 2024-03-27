import { Box } from '@chakra-ui/react';

const Footer = () => {
  return (
    <Box
      as="footer"
      bg="blue.500"
      color="white"
      py="4"
      px="8"
      position="fixed"
      bottom="0"
      left="0"
      right="0"
      zIndex="999"
    >
      Footer Content
    </Box>
  );
};

export default Footer;