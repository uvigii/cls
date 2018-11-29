<?php

namespace   MF\RingBundle\Controller;
use         MF\RingBundle\Entity\Ring;

class RingController extends Controller
{
    /**
     * @Route("/api/ring/new/{tel}", requirements={"id" = "\d+"})
     * @Template()
     */
    public function ringAction($tel)
    {
        
        $em = $this->getDoctrine()->getEntityManager();        
        $entity  = new Ring();
        $entity->setTel($tel);       
        $entity->setLogedat(new \DateTime);        
        $em->persist($entity);
        $em->flush();        
        
    }

    /**
     * Lists all Rings after Nth.
     *
     * @Route("/allnew/{id}", name="allnew", defaults={"id" = 0})
     * @Secure(roles="ROLE_USER") 
     * @Template()
     */
    public function allnewAction($id)
    {
        $htmldata = json_encode("");
        $em = $this->getDoctrine()->getEntityManager();
        $maxid = $em->createQuery("SELECT MAX(r.id) from MFRingBundle:Ring r ")->getSingleScalarResult();
        
        if ( $maxid > $id ){            
            $query = $em->createQuery("SELECT r from MFRingBundle:Ring r Order By r.id DESC")->setMaxResults(16);                                 
            $rings = $query->execute();
            $i = 0;
            foreach( $rings as $ring )
            {   
                if ($i < 4) {
                    $query2 = $em->createQuery("SELECT c from MFRingBundle:Contact c where c.tel like '%".$ring->getTel()."'");
                    $rc[]= array('caller' => $ring, 'contacts' => $query2->execute());
                } else {
                    $rg[] = $ring;
                }
                $i++;
            }
            $htmldata = json_encode(
                            $this->render('MFRingBundle:SSL:rings.html.twig', array('rings'=> $rc, 'tels' => $rg))->getContent() 
                    );
        }
        $response = new JsonResponse();
        $response->setData(array(
            'id' => $maxid,
            'html'  => $htmldata,
        ));
        $response->headers->set('Content-Type', 'application/json');
        return $response;
        //return array('rings' => $rc);
    }

}